using Elsa.Telnyx.Payloads.Abstract;

namespace Elsa.Telnyx.Payloads.Call;

public record CallMachineGreetingEndedBase : CallPayload
{
    public string Result { get; set; } = default!;
}