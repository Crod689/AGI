
//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


namespace AGIMaster.Models
{

using System;
    using System.Collections.Generic;
    
public partial class AdminOrder
{

    public int Id { get; set; }

    public int Order_Id { get; set; }

    public int AdminOrder_Id { get; set; }



    public virtual Order Order { get; set; }

    public virtual Order Order1 { get; set; }

}

}
