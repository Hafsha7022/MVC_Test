using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using ModelContextProtocol.Client;
using ModelContextProtocol.Protocol.Transport;
using System;
using System.Threading.Tasks;

namespace SKMCPClient
{
#pragma warning disable
    internal class Program
    {
        static async Task Main()
        {
            var clientTransport = new StdioClientTransport(new StdioClientTransportOptions
            {
                Name = "Local-MCP-Server",
                Command = "dotnet",
                Arguments =
                [
                    "run",
                    "--project",
                    //"C:\\Vinoth\\Delete\\SKSample\\SKSampleCSharp\\MCP\\LocalMCPServer",
                    "D:\\Hafsha\\GitHub\\Mvc_Test\\MVC_Test\\ProjectUpdate\\NewApp_SK\\MCP\\LocalMCPServer",
                    "--no-build"
                ],
            });

            var mcpClient = await McpClientFactory.CreateAsync(clientTransport);

            var tools = await mcpClient.ListToolsAsync().ConfigureAwait(false);

            foreach (var tool in tools)
            {
                Console.WriteLine($"{tool.Name}: {tool.Description}");
            }
            //Config config = new Config();
            //Console.WriteLine($"API Key from config: {config.ApiKey}");
            //Console.Read();
            //return;

            var kernel = CreateBuilder();

            kernel.Plugins.AddFromFunctions("LocalMCPServer",
                tools.Select(aiFunction => aiFunction.AsKernelFunction()));

            OpenAIPromptExecutionSettings executionSettings = new()
            {
                Temperature = 0,
                FunctionChoiceBehavior = FunctionChoiceBehavior.Auto(options: new() { RetainArgumentTypes = true })
            };

            var prompt = "list the details of books and weather information about Thanjavur";
            var result = await kernel.InvokePromptAsync(prompt, new(executionSettings)).ConfigureAwait(false);
            Console.WriteLine($"\n\n{prompt}\n{result}");

            Console.Read();
        }


        //private static Kernel CreateBuilder()
        //{
        //    Config config = new Config();

        //    var builder = Kernel.CreateBuilder();
        //    builder.Services.AddAzureOpenAIChatCompletion(config.DeploymentOrModelId, config.Endpoint, config.ApiKey);
        //    Kernel kernel = builder.Build();
        //    return kernel;
        //}

        private static Kernel CreateBuilder()
        {
            Config config = new Config();

            var builder = Kernel.CreateBuilder();
            // Use OpenAI instead of Azure OpenAI
            builder.Services.AddOpenAIChatCompletion(
                modelId: config.DeploymentOrModelId, // Should be OpenAI model name, e.g., "gpt-3.5-turbo"
                apiKey: config.ApiKey
            );
            Kernel kernel = builder.Build();
            return kernel;
        }
    }
}