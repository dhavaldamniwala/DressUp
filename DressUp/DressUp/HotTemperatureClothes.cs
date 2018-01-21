using DressUp.Interface;

namespace DressUp
{
    public class HotTemperatureClothes : IClothes
    {
        public string PutOnFootwear { get; set; } = "sandals";

        public string PutOnHeadwear { get; set; } = "sun visor";

        public string PutOnSocks { get; set; } = "fail";

        public string PutOnShirt { get; set; } = "t-shirt";

        public string PutOnJacket { get; set; } = "fail";

        public string PutOnPants { get; set; } = "shorts";

        public string LeaveHouse { get; set; } = "leaving house";

        public string TakeOffPajamas { get; set; } = "Removing PJs";
    }
}
