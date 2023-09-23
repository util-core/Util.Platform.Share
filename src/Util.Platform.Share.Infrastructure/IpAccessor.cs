using Util.Helpers;

namespace Util.Platform.Share; 

/// <summary>
/// IP访问器
/// </summary>
public class IpAccessor : IIpAccessor {
    /// <inheritdoc />
    public string GetIp() {
        return Ip.GetIp();
    }
}