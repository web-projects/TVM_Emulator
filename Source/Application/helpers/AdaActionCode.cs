using static System.ExtensionMethods;

namespace TVMEmulator.helpers
{
    public enum AdaActionCode
    {
        [StringValue("Start ADA Mode")]
        StartAdaMode,
        [StringValue("Update Message")]
        UpdateMessage,
        [StringValue("End ADA Mode")]
        EndAdaMode
    }
}
