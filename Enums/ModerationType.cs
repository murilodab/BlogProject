using System.ComponentModel;

namespace BlogProject.Enums
{
    public enum ModerationType
    {
        [Description("Political Propaganda")]
        Political,
        [Description("Offensive Language")]
        Language,
        [Description("Drug References")]
        Drugs,
        [Description("Threatening Speech")]
        Threatening,
        [Description("Sexual Content")]
        Sexual,
        [Description("Hate Speech")]
        HateSpeech,
        [Description("Target Shaming")]
        Shaming
    }
}
