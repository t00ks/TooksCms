using System;

namespace TooksCms.Core.Interfaces
{
    public interface IRating : IInterfacingBase
    {
        int RatingId { get; }
        Guid RatingUid { get; }
        string Name { get; }
    }
}
