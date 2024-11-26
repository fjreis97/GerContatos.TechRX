﻿namespace Business.Dtos.Request;

public class PagedRequest
{
    public int PageNumber { get; set; } = Configuration.DefaultPageNumber;
    public int PageSize { get; set; } = Configuration.DefaultPageSize;
}
