using System;
using System.Collections.Generic;
using System.Text;

namespace Example.Helpers
{
    public class UsersResourceParameter
    {
        /// <summary>
        /// Used to limit the page size of a returned list
        /// </summary>
        private const int maxPageSize = 20;

        public int PageNumber { get; set; }

        private int pageSize = 10;

        public int PageSize
        {
            get => this.pageSize;
            set => this.pageSize = value > maxPageSize ? maxPageSize : value;
        }
    }
}
