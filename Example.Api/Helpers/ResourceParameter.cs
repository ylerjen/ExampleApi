namespace Example.Api.Helpers
{
    public class ResourceParameter
    {
        private const uint MaxPageSize = 20;

        /// <summary>
        /// The page number we want to display
        /// </summary>
        /// <value>The page range starts with 1</value>
        public uint PageNumber { get; set; } = 1;

        private uint pageSize = 10;

        /// <summary>
        /// Gets or sets the pageSize which is the number of item to display in a page.
        /// </summary>
        /// <value>If the value exceed the <see cref="MaxPageSize"/> value, this last value will be used.</value>
        public uint PageSize
        {
            get => this.pageSize;
            set => this.pageSize = value > MaxPageSize ? MaxPageSize : value;
        }
    }
}
