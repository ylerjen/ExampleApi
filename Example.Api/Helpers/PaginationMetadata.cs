using Newtonsoft.Json;

namespace Example.Api.Helpers
{
    public class PaginationMetadata
    {
        public PaginationMetadata(uint totalCount, uint pageSize, uint currentPage, uint totalPages, string previousPageLink, string nextPageLink)
        {
            this.TotalCount = totalCount;
            this.PageSize = pageSize;
            this.CurrentPage = currentPage;
            this.TotalPages = totalPages;
            this.PreviousPageLink = previousPageLink;
            this.NextPageLink = nextPageLink;
        }

        [JsonProperty(PropertyName = "totalCount")]
        public uint TotalCount { get; set; }

        [JsonProperty(PropertyName = "pageSize")]
        public uint PageSize { get; set; }

        [JsonProperty(PropertyName = "currentPage")]
        public uint CurrentPage { get; set; }

        [JsonProperty(PropertyName = "totalPages")]
        public uint TotalPages { get; set; }

        [JsonProperty(PropertyName = "previousPageLink")]
        public string PreviousPageLink { get; set; }

        [JsonProperty(PropertyName = "nextPageLink")]
        public string NextPageLink { get; set; }
    }
}
