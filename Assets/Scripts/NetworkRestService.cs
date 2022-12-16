using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using Proyecto26;
using Newtonsoft.Json;
using UnityEngine;

public enum HTTPMethod
{
    GET,
    POST,
    PATCH,
    DELETE,
    PUT,
}
public class NetworkRestService
{
    private bool isDebug;

    public NetworkRestService(bool isDebug)
    {
        this.isDebug = isDebug;
    }

    public UniTask<T> Get<T>(string baseUrl, string path, Dictionary<string, string> parameters = null, Dictionary<string, string> Headers = null)
    {
        RequestHelper requestOptions = this.CreateRequestHelper(
                HTTPMethod.GET,
                baseUrl,
                path,
                parameters,
                Headers
            );
        UniTaskCompletionSource<T> taskCompletionSource = new UniTaskCompletionSource<T>();

        RestClient.Request(requestOptions).Then(response =>
        {
            var json = response.Request.downloadHandler.text;
            var result = JsonConvert.DeserializeObject<T>(json);
            taskCompletionSource.TrySetResult(result);
        }).Catch(err =>
        {
            Debug.LogError($"{err.GetType().FullName}, {err.Message}");
            taskCompletionSource.TrySetException(err);
        });

        return taskCompletionSource.Task;
    }

    public UniTask<T> Post<T>(string baseUrl, string path, object body, Dictionary<string, string> Headers)
    {
        RequestHelper requestOptions = this.CreateRequestHelper(
                HTTPMethod.POST,
                baseUrl,
                path,
                null,
                Headers
            );
        requestOptions.Body = body;

        UniTaskCompletionSource<T> taskCompletionSource = new UniTaskCompletionSource<T>();

        RestClient.Request(requestOptions).Then(response =>
        {
            string json = response.Request.downloadHandler.text;
            T result = JsonConvert.DeserializeObject<T>(json);
            taskCompletionSource.TrySetResult(result);
        }).Catch(err =>
        {
            Debug.LogError($"{err.GetType().FullName}, {err.Message}");
            taskCompletionSource.TrySetException(err);
        });

        return taskCompletionSource.Task;
    }

    private RequestHelper CreateRequestHelper(HTTPMethod method, string baseUrl, string path, Dictionary<string, string> parameters, Dictionary<string, string> Headers)
    {
        string uri = baseUrl + "/" + path;
        RequestHelper requestOptions = new RequestHelper
        {
            Uri = uri,
            Headers = Headers,
            Method = method.ToString(),
            Params = parameters,
            EnableDebug = isDebug
        };
        return requestOptions;
    }
}
