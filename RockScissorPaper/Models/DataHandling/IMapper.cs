﻿using System.Collections.Generic;
using System.Data;

namespace RockScissorPaper.Models.DataHandling
{
    /// <summary>
    /// Interface For Database mapper class.
    /// Map method will map the provided datatable into desired object;
    /// </summary>
    public interface IMapper
    {
        object Result { get; }
        List<object> Results { get; }
        /// <summary>
        /// Maps Datatable to object
        /// Can use Proceedure string for function.
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="sqlProceedureString"></param>
        void Map(DataTable dt, string sqlProceedureString);
    }
}