
using SuriWebhook.Models;
using SuriWebhook.Services;
using System.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json", optional: false);

builder.Services.AddScoped<DatabaseService>();
builder.Services.AddScoped<CPFService>();

var app = builder.Build();

var serverApiKey = builder.Configuration["AppSettings:serverApiKey"];
var serverUrl = builder.Configuration["AppSettings:serverUrl"];
var connectionString = builder.Configuration["ConnectionString:Default"];

CPFService cPFService = new CPFService(new DatabaseService(builder.Configuration));


app.MapGet("/", () => "Welcome to Suri Webhook!");

app.MapGet("/webhook", () => "cb6262");

app.MapPost("/webhook", async context => {
    var response = context.Response;
    response.StatusCode = StatusCodes.Status200OK;
    var requestBody = await context.Request.ReadFromJsonAsync<GenericWebhookPayload>();
    Console.WriteLine(requestBody);
    if (requestBody!.payload.user.CurrentDialog.Uri.Parameters.intent == "Segunda via")
        {
            string cpf = requestBody.payload.Message.text;
            if (cPFService.validateCPF(cpf))
            {
                string segundaVia = cPFService.GetPDFByCPF(cpf);
                if(segundaVia != null)
                {
                    var client = new HttpClient();
                    var request = new HttpRequestMessage(HttpMethod.Post, $"{serverUrl}/messages/send");
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", serverApiKey);
                    string jsonContent = $@"{{
                        ""userId"": ""{requestBody.payload.user.Id}"",
                        ""message"": {{
                            ""attachment"": {{
                                ""type"": ""file"",
                                ""fileName"": ""SegundaVia{cpf}"",
                                ""payload"": {{
                                    ""url"": ""{segundaVia}""
                                }}
                            }}
                        }},
                        ""isTemplate"": false
                    }}";

                    request.Content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");

                    var res = await client.SendAsync(request);
                    res.EnsureSuccessStatusCode();
                    Console.WriteLine(await res.Content.ReadAsStringAsync());

                    var returnContato = new HttpRequestMessage(HttpMethod.Get, $"{serverUrl}/contacts/{requestBody.payload.user.Id}/backtosuri/");
                    var backToSuri = await client.SendAsync(request);
                    backToSuri.EnsureSuccessStatusCode();
                    Console.WriteLine(await backToSuri.Content.ReadAsStringAsync());

                } else 
                {
                    var client = new HttpClient();
                    var request = new HttpRequestMessage(HttpMethod.Post, $"{serverUrl}/messages/send");
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", serverApiKey);
                    string jsonContent = $@"{{
                            ""userId"": ""{requestBody.payload.user.Id}"",
                            ""message"": {{
                                ""text"": ""Seu arquivo não foi encontrado em nossa base de dados!""  
                                }}
                            }},
                            ""isTemplate"": false
                        }}";

                    request.Content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");
                    var res = await client.SendAsync(request);
                    res.EnsureSuccessStatusCode();
                    Console.WriteLine(await res.Content.ReadAsStringAsync());
                    var returnContato = new HttpRequestMessage(HttpMethod.Get, $"{serverUrl}/contacts/{requestBody.payload.user.Id}/backtosuri/");
                    var backToSuri = await client.SendAsync(request);
                    backToSuri.EnsureSuccessStatusCode();
                    Console.WriteLine(await backToSuri.Content.ReadAsStringAsync());
                }
            } else
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, $"{serverUrl}/messages/send");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", serverApiKey);
                string jsonContent = $@"{{
                                ""userId"": ""{requestBody.payload.user.Id}"",
                                ""message"": {{
                                    ""text"": ""O CPF inserido não é um CPF válido.""  
                                    }}
                                }},
                                ""isTemplate"": false
                            }}";

                request.Content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");
                var res = await client.SendAsync(request);
                res.EnsureSuccessStatusCode();
                Console.WriteLine(await res.Content.ReadAsStringAsync());
                var returnContato = new HttpRequestMessage(HttpMethod.Get, $"{serverUrl}/contacts/{requestBody.payload.user.Id}/backtosuri/");
                var backToSuri = await client.SendAsync(request);
                backToSuri.EnsureSuccessStatusCode();
                Console.WriteLine(await backToSuri.Content.ReadAsStringAsync());
              }
        };
});

app.Run();


