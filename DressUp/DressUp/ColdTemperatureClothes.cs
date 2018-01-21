using DressUp.Interface;

namespace DressUp
{
    public class ColdTemperatureClothes : IClothes
    {
        public string PutOnFootwear { get; set; } = "boots";

        public string PutOnHeadwear { get; set; } = "hat";
        
        public string PutOnSocks { get; set; } = "socks";
        
        public string PutOnShirt { get; set; } = "shirt";
        
        public string PutOnJacket { get; set; } = "jacket";
        
        public string PutOnPants { get; set; } = "pants";
        
        public string LeaveHouse { get; set; } = "leaving house";
        
        public string TakeOffPajamas { get; set; } = "Removing PJs";
    }
}
