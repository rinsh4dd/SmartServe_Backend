using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SmartServe.Application.Common;
using SmartServe.Application.Contracts.Repository;
using SmartServe.Application.Contracts.Services;
using SmartServe.Application.DTOs;
using System.Net.Http;
using System.Text;

public class AIService : IAIService
{
    private readonly IAIRepository _repo;
    private readonly HttpClient _http;
    private readonly string _apiKey;
    private readonly string _model;

    public AIService(IAIRepository repo, IConfiguration config, HttpClient http)
    {
        _repo = repo;
        _http = http;

        _apiKey = config["GeminiSettings:ApiKey"];
        _model = config["GeminiSettings:Model"];
    }

    public async Task<ApiResponse<VehicleAIResponseDto>> GenerateVehicleAIReportAsync(int vehicleId)
    {
        var data = await _repo.GetVehicleAIDataAsync(vehicleId);

        if (data.Vehicle == null)
            return new ApiResponse<VehicleAIResponseDto>(404, "Vehicle not found");

        string json = JsonConvert.SerializeObject(data, Formatting.Indented);
        string prompt = $@"
You are an automotive diagnostic AI of SmartServeErp. Analyze the following vehicle data.
Return STRICT JSON:
{{
  ""overview"": string,
  ""recentIssues"": string[],
  ""serviceSummary"": string,
  ""predictedFailures"": string[],
  ""recommendedMaintenance"": string[],
  ""currentVehichleCondition"":stirng[],
  ""healthScore"": number
}}

DATA:
{json}
";

        string url =
            $"https://generativelanguage.googleapis.com/v1beta/{_model}:generateContent?key={_apiKey}";
        var body = new
        {
            contents = new[]
            {
                new {
                    role = "user",
                    parts = new[] { new { text = prompt } }
                }
            }
        };

        var payload = new StringContent(
            JsonConvert.SerializeObject(body),
            Encoding.UTF8,
            "application/json"
        );

        HttpResponseMessage response;
        try
        {
            response = await _http.PostAsync(url, payload);
        }
        catch (TaskCanceledException)
        {
            return new ApiResponse<VehicleAIResponseDto>(408, "AI request timed out");
        }

        string responseText = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            return new ApiResponse<VehicleAIResponseDto>(
                500,
                "AI service error",
                new VehicleAIResponseDto { AIReport = responseText }
            );
        }

        string aiText = ExtractGeminiText(responseText);
        aiText = CleanAIOutput(aiText);

        return new ApiResponse<VehicleAIResponseDto>(
            200,
            "AI report generated",
            new VehicleAIResponseDto { AIReport = aiText }
        );
    }
    private string CleanAIOutput(string text)
    {
        if (string.IsNullOrWhiteSpace(text)) return text;

        return text
            .Replace("```json", "")
            .Replace("```", "")
            .Trim();
    }

    private string ExtractGeminiText(string rawJson)
    {
        try
        {
            dynamic obj = JsonConvert.DeserializeObject(rawJson);

            string? text =
                obj?.candidates?[0]?.content?.parts?[0]?.text ??
                obj?.candidates?[0]?.content?[0]?.parts?[0]?.text ??
                obj?.output_text ??
                obj?.predictions?[0]?.content?[0]?.text ??
                null;

            return text ?? rawJson;
        }
        catch
        {
            return rawJson;
        }
    }
}
