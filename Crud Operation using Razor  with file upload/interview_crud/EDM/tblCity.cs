//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace interview_crud.EDM
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblCity
    {
        public int City_id { get; set; }
        public string City_name { get; set; }
        public Nullable<int> State_id { get; set; }
    
        public virtual tblState tblState { get; set; }
    }
}
