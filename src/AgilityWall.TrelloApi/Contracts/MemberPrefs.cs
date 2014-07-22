namespace PortableTrello.Contracts
{
    public class MemberPrefs
    {
        public bool SendSummaries { get; set; }
        public int MinutesBetweenSummaries { get; set; }
        public int MinutesBeforeDeadlineToNotify { get; set; }
        public bool ColorBlind { get; set; }
        public MemberTimezoneInfo TimezoneInfo { get; set; }
    }
}