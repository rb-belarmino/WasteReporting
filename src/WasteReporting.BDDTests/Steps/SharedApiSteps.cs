using TechTalk.SpecFlow;
using RestSharp;
using FluentAssertions;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace WasteReporting.BDDTests.Steps
{
    [Binding]
    public class SharedApiSteps
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly RestClient _client;

        public SharedApiSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            var apiUrl = Environment.GetEnvironmentVariable("API_URL") ?? "http://localhost:5001";
            _client = new RestClient(apiUrl);
        }

        [Given(@"que eu tenho um payload JSON:")]
        [When(@"que eu tenho um payload JSON:")]
        public void DadoQueEuTenhoUmPayloadJSON(string multilineText)
        {
            _scenarioContext["Payload"] = multilineText;
        }

        [Given(@"que eu não tenho payload")]
        [When(@"que eu não tenho payload")]
        [Then(@"que eu não tenho payload")]
        public void DadoQueEuNaoTenhoPayload()
        {
            if (_scenarioContext.ContainsKey("Payload"))
            {
                _scenarioContext.Remove("Payload");
            }
        }

        [Given(@"que eu tenho um payload de login com email ""(.*)"" e senha ""(.*)""")]
        public void DadoQueEuTenhoUmPayloadDeLoginComEmailESenha(string email, string password)
        {
            _scenarioContext["Payload"] = new { Email = email, Password = password };
        }

        [When(@"eu consultar o endpoint de auth ""(.*)""")]
        public async Task QuandoEuConsultarOEndpointDeAuth(string endpoint)
        {
            var request = new RestRequest(endpoint, Method.Post);
            if (_scenarioContext.TryGetValue("Payload", out object payload))
            {
                if (payload is string jsonString)
                    request.AddStringBody(jsonString, DataFormat.Json);
                else
                    request.AddJsonBody(payload);
            }
            var response = await _client.ExecuteAsync(request);
            _scenarioContext["Response"] = response;
        }

        [Then(@"o status code deve ser (.*)")]
        public void EntaoOStatusCodeDeveSer(int statusCode)
        {
            var response = _scenarioContext.Get<RestResponse>("Response");
            ((int)response.StatusCode).Should().Be(statusCode);
        }

        [Then(@"o corpo da resposta deve validar o schema JSON ""(.*)""")]
        public void EntaoOCorpoDaRespostaDeveValidarOSchemaJSON(string schemaFileName)
        {
            var response = _scenarioContext.Get<RestResponse>("Response");
            var schemaPath = Path.Combine("Schemas", schemaFileName);
            var schemaJson = File.ReadAllText(schemaPath);
            JSchema schema = JSchema.Parse(schemaJson);

            JToken responseToken = JToken.Parse(response.Content!);
            bool isValid = responseToken.IsValid(schema, out IList<string> errors);
            isValid.Should().BeTrue("Erros de schema: " + string.Join(", ", errors));
        }

        [Given(@"que eu estou autenticado como ""(.*)"" com senha ""(.*)""")]
        public async Task DadoQueEuEstouAutenticadoComoComSenha(string email, string password)
        {
            var registerRequest = new RestRequest("/auth/register", Method.Post);
            registerRequest.AddJsonBody(new { Username = "Cidadao Test", Email = email, Password = password });
            await _client.ExecuteAsync(registerRequest);

            var loginRequest = new RestRequest("/auth/login", Method.Post);
            loginRequest.AddJsonBody(new { Email = email, Password = password });
            
            var response = await _client.ExecuteAsync(loginRequest);
            if (response.IsSuccessful)
            {
                var json = JObject.Parse(response.Content!);
                _scenarioContext["Token"] = json["token"]?.ToString();
            }
        }

        [Given(@"que eu tenho um payload de relato com localização ""(.*)"" e descrição ""(.*)""")]
        public void DadoQueEuTenhoUmPayloadDeRelatoComLocalizacaoEDescricao(string location, string description)
        {
            _scenarioContext["Payload"] = new { Location = location, Description = description };
        }

        [When(@"eu enviar um POST para o endpoint ""(.*)""")]
        public async Task QuandoEuEnviarUmPOSTParaOEndpoint(string endpoint)
        {
            var request = new RestRequest(endpoint, Method.Post);
            
            if (_scenarioContext.TryGetValue("Token", out string token))
            {
                request.AddHeader("Authorization", $"Bearer {token}");
            }

            if (_scenarioContext.TryGetValue("Payload", out object payload))
            {
                if (payload is string jsonString)
                    request.AddStringBody(jsonString, DataFormat.Json);
                else
                    request.AddJsonBody(payload);
            }
            
            var response = await _client.ExecuteAsync(request);
            _scenarioContext["Response"] = response;
        }

        [When(@"eu enviar um GET para o endpoint ""(.*)""")]
        public async Task QuandoEuEnviarUmGETParaOEndpoint(string endpoint)
        {
            var request = new RestRequest(endpoint, Method.Get);
            
            if (_scenarioContext.TryGetValue("Token", out string token))
            {
                request.AddHeader("Authorization", $"Bearer {token}");
            }
            
            var response = await _client.ExecuteAsync(request);
            _scenarioContext["Response"] = response;
        }

        [When(@"eu enviar um PUT para o endpoint ""(.*)""")]
        public async Task QuandoEuEnviarUmPUTParaOEndpoint(string endpoint)
        {
            var request = new RestRequest(endpoint, Method.Put);
            
            if (_scenarioContext.TryGetValue("Token", out string token))
            {
                request.AddHeader("Authorization", $"Bearer {token}");
            }

            if (_scenarioContext.TryGetValue("Payload", out object payload))
            {
                if (payload is string jsonString)
                    request.AddStringBody(jsonString, DataFormat.Json);
                else
                    request.AddJsonBody(payload);
            }
            
            var response = await _client.ExecuteAsync(request);
            _scenarioContext["Response"] = response;
        }

        [When(@"eu enviar um DELETE para o endpoint ""(.*)""")]
        public async Task QuandoEuEnviarUmDELETEParaOEndpoint(string endpoint)
        {
            var request = new RestRequest(endpoint, Method.Delete);
            
            if (_scenarioContext.TryGetValue("Token", out string token))
            {
                request.AddHeader("Authorization", $"Bearer {token}");
            }
            
            var response = await _client.ExecuteAsync(request);
            _scenarioContext["Response"] = response;
        }
    }
}
