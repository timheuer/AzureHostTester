var builder = DistributedApplication.CreateBuilder(args);

// container
var container1 = builder.AddContainer("maildev", "maildev/maildev", "2.0.2");

// Azure
var appconfig = builder.AddAzureAppConfiguration("appconfig");
var appinsights = builder.AddAzureApplicationInsights("appinsights");
var openAI = builder.AddAzureOpenAI("oai")
            .AddDeployment(new AzureOpenAIDeployment("gpt3516k", "gpt-35-turbo-16k", "0613"))
            .AddDeployment(new AzureOpenAIDeployment("textada2", "text-embedding-ada-002", "2"));
var cosmos = builder.AddAzureCosmosDB("cosmos")
    .AddDatabase("db1");
var events = builder.AddAzureEventHubs("events")
    .AddEventHub("hub1");
var kv = builder.AddAzureKeyVault("kv");
var logai = builder.AddAzureLogAnalyticsWorkspace("law");
var postgres = builder.AddPostgres("postgres")
    .AsAzurePostgresFlexibleServer();
var redis = builder.AddRedis("redis")
    .AsAzureRedis();
var search = builder.AddAzureSearch("search");
var servicebus = builder.AddAzureServiceBus("servicebus")
    .AddQueue("queue1")
    .AddSubscription("topic1","sub1")
    .AddTopic("topic1");
var signalr = builder.AddAzureSignalR("signalr");
var sql = builder.AddSqlServer("sql")
    .AsAzureSqlDatabase()
    .AddDatabase("db2");
var storage = builder.AddAzureStorage("storage");
var blobs = storage.AddBlobs("blobs");
var queues = storage.AddQueues("queues");
var tables = storage.AddTables("tables");

builder.AddProject<Projects.WebApplication1>("webapplication1")
    .WithReference(tables)
    .WithReference(queues)
    .WithReference(blobs)
    .WithReference(sql)
    .WithReference(signalr)
    .WithReference(appconfig)
    .WithReference(appinsights)
    .WithReference(openAI)
    .WithReference(cosmos)
    .WithReference(events)
    .WithReference(kv)
    .WithReference(redis)
    .WithReference(postgres)
    .WithReference(search)
    .WithReference(servicebus)
    .WithReference(sql);

builder.Build().Run();
