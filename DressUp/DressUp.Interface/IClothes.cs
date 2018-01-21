namespace DressUp.Interface
{
    /// <summary>
    ///     Interface for Clothes
    /// </summary>
    public interface IClothes
    {
        /// <summary>
        ///     Get description for PutOnFootwear
        /// </summary>
        string PutOnFootwear { get; set; }

        /// <summary>
        ///     Get description for Put On Headwear
        /// </summary>
        string PutOnHeadwear { get; set; }

        /// <summary>
        ///     Get description for Put On Socks
        /// </summary>
        string PutOnSocks { get; set; }

        /// <summary>
        ///     Get description for Put On Shirt
        /// </summary>
        string PutOnShirt { get; set; }

        /// <summary>
        ///     Get description for Put On Jacket
        /// </summary>
        string PutOnJacket { get; set; }

        /// <summary>
        ///     Get description for Put On Pants
        /// </summary>
        string PutOnPants { get; set; }

        /// <summary>
        ///     Get description for Leave House
        /// </summary>
        string LeaveHouse { get; set; }

        /// <summary>
        ///     Get description for Take Off Pajamas
        /// </summary>
        string TakeOffPajamas { get; set; }
    }
}
