using Newtonsoft.Json;

namespace MyFinances.Common.Core.Email.Brevo;

public class DynamicEmailDto
{
    [JsonProperty("templateId")]
    public int TemplateId { get; set; }
    [JsonProperty("to")]
    public List<ToDto> To { get; set; }
    [JsonProperty("params")]
    public ParamsDto Params { get; set; }
}

public class ToDto
{
    public string Email { get; set; }
}

public class ParamsDto
{
    [JsonProperty("VERIFICATIONCODE")]
    public string VerificationCode { get; set; }
}