using Android.App;
using Android.Telecom;
using CallShield.Common.Models;
using CallShield.UI.Processors;

namespace CallShield.UI.Platforms.Android.Services;

[Service(Exported = true, Permission = "android.permission.BIND_SCREENING_SERVICE")]
[IntentFilter(["android.telecom.CallScreeningService"])]
public class SpamCallBlockingService : CallScreeningService
{
    private readonly CallProcessor _callProcessor;

    public SpamCallBlockingService()
    {
        _callProcessor = new CallProcessor();
    }

    public const string DefaultCallerDisplayName = "Unverified Caller";
    public const string DefaultPhoneNumber = "Unknown";

    public override void OnScreenCall(Call.Details callDetails)
    {
        if (callDetails.CallDirection != CallDirection.Incoming)
        {
            return;
        }

        var responseBuilder = new CallResponse.Builder();

        switch (callDetails.CallerNumberVerificationStatus)
        {
            case (int)ConnectionVerificationStatusType.VerificationStatusFailed:
            case (int)ConnectionVerificationStatusType.VerificationStatusNotVerified:
                responseBuilder.SetDisallowCall(true);
                responseBuilder.SetRejectCall(true);
                responseBuilder.SetSkipNotification(true);
                break;
        }
        
        var callResponse = responseBuilder.Build();

        // For incoming calls, we must make this call within 5 seconds of being bound.
        RespondToCall(callDetails, callResponse!);

        var phoneNumber = DefaultPhoneNumber;
        var callHandle = callDetails.GetHandle();
        
        if (callHandle is not null && callHandle.SchemeSpecificPart is not null)
        {
            phoneNumber = callHandle.SchemeSpecificPart;
        }


        _callProcessor.NotifyOfBlockedCall(new CallDetails(
            callDetails.CallerDisplayName ?? DefaultCallerDisplayName,
            phoneNumber,
            DateTime.Now));
    }
}
