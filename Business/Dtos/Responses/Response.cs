﻿using System.Text.Json.Serialization;

namespace Business.Dtos.Responses;

public class Response<TData>
{
    private readonly int _code;
    private const int DefaultStatusCode = 200;

    [JsonConstructor]
    public Response() => _code = DefaultStatusCode;

    public Response(TData? data, int code = DefaultStatusCode, string? message = null)
    {
        Data = data;
        Message = message;
        _code = code;
    }

    public TData? Data { get; set; }
    public string? Message { get; set; }
    [JsonIgnore]
    public bool IsSuccess => _code is >= 200 and <= 299;
}
