
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SunriseWeb.Models
{
    public class CompareStoneResult
    {
        public List<string> ReferenceNo { get; set; }
        public List<string> Imge1 { get; set; }
        public List<string> Status { get; set; }
        public List<string> Shape { get; set; }
        public List<string> Lab { get; set; }
        public List<string> CertiNo { get; set; }
        public List<string> Shade { get; set; }
        public List<string> Color { get; set; }
        public List<string> Clarity { get; set; }
        public List<string> CaratWeight { get; set; }
        public List<string> RapPrice { get; set; }
        public List<string> RapAmt { get; set; }
        public List<string> Disc { get; set; }
        public List<string> Cut { get; set; }
        public List<string> Polish { get; set; }
        public List<string> Symmetry { get; set; }
        public List<string> Flurescence { get; set; }
        public List<string> Length { get; set; }
        public List<string> Width { get; set; }
        public List<string> Depth { get; set; }
        public List<string> TotalDepth { get; set; }
        public List<string> Table { get; set; }
        public List<string> KeytoSymbol { get; set; }
        public List<string> table_natts { get; set; }
        public List<string> Crown_Natts { get; set; }
        public List<string> inclusion { get; set; }
        public List<string> Crown_Inclusion { get; set; }
        public List<string> CrAng { get; set; }
        public List<string> CrHt { get; set; }
        public List<string> PavAng { get; set; }
        public List<string> PavHt { get; set; }
        public List<string> GirdleType { get; set; }
        public List<string> net_amount { get; set; }

        public CompareStoneResult()
        {
            ReferenceNo = new List<string>();
            Imge1 = new List<string>();
            Status = new List<string>();
            Shape = new List<string>();
            Lab = new List<string>();
            CertiNo = new List<string>();
            Shade = new List<string>();
            Color = new List<string>();
            Clarity = new List<string>();
            CaratWeight = new List<string>();
            RapPrice = new List<string>();
            RapAmt = new List<string>();
            Disc = new List<string>();
            Cut = new List<string>();
            Polish = new List<string>();
            Symmetry = new List<string>();
            Flurescence = new List<string>();
            Length = new List<string>();
            Width = new List<string>();
            Depth = new List<string>();
            TotalDepth = new List<string>();
            Table = new List<string>();
            KeytoSymbol = new List<string>();
            table_natts = new List<string>();
            Crown_Natts = new List<string>();
            inclusion = new List<string>();
            Crown_Inclusion = new List<string>();
            CrAng = new List<string>();
            CrHt = new List<string>();
            PavAng = new List<string>();
            PavHt = new List<string>();
            GirdleType = new List<string>();
            net_amount = new List<string>();
        }
    }
}