//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication10.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class feedback
    {
        public int Id { get; set; }
        public Nullable<int> teamLeaderId { get; set; }
        public Nullable<int> traineeId { get; set; }
        public Nullable<int> projectId { get; set; }
        public Nullable<int> rate { get; set; }
        public byte[] time { get; set; }
    
        public virtual users users { get; set; }
        public virtual users users1 { get; set; }
        public virtual projects projects { get; set; }
    }
}
