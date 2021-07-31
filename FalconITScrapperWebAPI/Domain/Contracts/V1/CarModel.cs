using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Contracts.V1
{
    public class CarModel
    {
        public CarModel()
        {
            data = new CarModelData();
        }
        public bool Success { get; set; } = false;
        public string[] Message { get; set; }
        public CarModelData data { get; set; }
    }
    public class CarModelData
    {
       
        public string licensePlate { get; set; }
        public string Mark { get; set; }
        public string Range { get; set; }
        public string Model { get; set; }
        public string Phase { get; set; }
        public string Series { get; set; }
        public string Wording { get; set; }
        public string Body { get; set; }
        public string TypeMines { get; set; }
        public string WINE { get; set; }
        public string Motor { get; set; }
        public string Color { get; set; }
        public string Release { get; set; }
        public string StartofMarketing { get; set; }
        public string EndofMarketing { get; set; }
    }
}
