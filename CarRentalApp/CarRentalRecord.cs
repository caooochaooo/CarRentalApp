//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CarRentalApp
{
    using System;
    using System.Collections.Generic;
    
    public partial class CarRentalRecord
    {
        public int id { get; set; }
        public string CustomerName { get; set; }
        public Nullable<System.DateTime> DateRented { get; set; }
        public Nullable<System.DateTime> DateReturned { get; set; }
        public Nullable<decimal> Cost { get; set; }
        public Nullable<int> TypeOfCarID { get; set; }
    
        public virtual TypeOfCar TypeOfCar { get; set; }
    }
}