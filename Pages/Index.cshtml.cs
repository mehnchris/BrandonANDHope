using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text;

namespace WeddingSite.Pages;

public class IndexModel : PageModel
{
    private readonly IWebHostEnvironment _environment;

    public IndexModel(IWebHostEnvironment environment)
    {
        _environment = environment;
    }

    [BindProperty]
    public RsvpInput Rsvp { get; set; } = new();

    public WeddingContent Wedding { get; } = WeddingContent.FromIntake();

    public bool RsvpSubmitted { get; private set; }

    public void OnGet()
    {
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        SaveRsvp();
        RsvpSubmitted = true;
        ModelState.Clear();
        Rsvp = new();
        return Page();
    }

    private void SaveRsvp()
    {
        var dataDirectory = Path.Combine(_environment.ContentRootPath, "App_Data");
        Directory.CreateDirectory(dataDirectory);

        var path = Path.Combine(dataDirectory, "Rsvps.csv");
        var includeHeader = !System.IO.File.Exists(path);
        var builder = new StringBuilder();

        if (includeHeader)
        {
            builder.AppendLine("SubmittedAtUtc,FullName,Email,Attendance,PlusOneName,SongRequest,Note");
        }

        var values = new[]
        {
            DateTime.UtcNow.ToString("O", CultureInfo.InvariantCulture),
            Rsvp.FullName,
            Rsvp.Email,
            Rsvp.Attendance,
            Rsvp.PlusOneName,
            Rsvp.SongRequest,
            Rsvp.Note
        };

        builder.AppendLine(string.Join(",", values.Select(EscapeCsv)));
        System.IO.File.AppendAllText(path, builder.ToString());
    }

    private static string EscapeCsv(string value)
    {
        var normalized = value.Replace("\"", "\"\"");
        return $"\"{normalized}\"";
    }
}

public class RsvpInput
{
    [Required]
    [Display(Name = "Guest name")]
    public string FullName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    [Display(Name = "Will you attend?")]
    public string Attendance { get; set; } = string.Empty;

    [Display(Name = "Plus-one name")]
    public string PlusOneName { get; set; } = string.Empty;

    [Display(Name = "Song request")]
    public string SongRequest { get; set; } = string.Empty;

    [Display(Name = "Note to the couple")]
    public string Note { get; set; } = string.Empty;
}

public sealed record WeddingContent(
    string GroomName,
    string BrideName,
    string WeddingDate,
    string VenueName,
    string VenueAddress,
    string VenueCity,
    string DressCode,
    string CeremonyTime,
    string ReceptionTime,
    string RsvpDeadline,
    string HeroImageUrl,
    string StoryIntro,
    string CelebrationCopy,
    string ParkingNote,
    string TravelNote,
    IReadOnlyList<string> Hotels,
    IReadOnlyList<string> LocalRecommendations,
    IReadOnlyList<StoryMoment> Story,
    IReadOnlyList<ScheduleItem> Schedule,
    IReadOnlyList<FaqItem> Faqs)
{
    public string CoupleNames => $"{GroomName} & {BrideName}";

    public static WeddingContent FromIntake() => new(
        GroomName: "Brandon Davis-Barkus",
        BrideName: "Hope Leyva",
        WeddingDate: "Friday, April 23, 2027",
        VenueName: "Lakeside Wedding Venue",
        VenueAddress: "33001 Lake Rd, Shawnee, OK 74801",
        VenueCity: "Shawnee, Oklahoma",
        DressCode: "Formal",
        CeremonyTime: "5:00 PM",
        ReceptionTime: "5:30 PM",
        RsvpDeadline: "February 28, 2027",
        HeroImageUrl: "https://images.unsplash.com/photo-1523438885200-e635ba2c371e?auto=format&fit=crop&w=1800&q=85",
        StoryIntro: "Their story started at the gym and quickly became the kind of connection neither of them wanted to put down.",
        CelebrationCopy: "A modern romantic celebration with playful Mexican wedding inspiration, warm color, lakeside views, and the people Brandon and Hope love most.",
        ParkingNote: "Parking is available at the front of the venue inside the gate.",
        TravelNote: "For guests flying in, Will Rogers World Airport in Oklahoma City is the recommended airport.",
        Hotels:
        [
            "The Grand",
            "Hampton Inn",
            "Fairfield by Marriott Inn"
        ],
        LocalRecommendations:
        [
            "Tapatio Mexican Restaurant",
            "Cinema 8",
            "The Grand Casino",
            "Nearby Shawnee-area casinos"
        ],
        Story:
        [
            new("The Gym", "Where It Started", "Brandon and Hope first met at the gym, where an ordinary day turned into the beginning of their life together."),
            new("June 25, 2021", "A Road Trip To Remember", "Their first memorable date was a summer road trip with no A/C, a stop in Bricktown, dinner at Abuelo's, ice cream, and hours talking in front of Sonic."),
            new("August 2021", "The L Word", "By late summer, they had both said what they were already feeling: this was love."),
            new("April 2022", "Zeus Joins The Story", "They got their dog Zeus together, adding more laughter and love to their everyday life."),
            new("Their Bond", "Always Laughing", "What they love most is the silliness, the laughter, the affection that has never faded, and the way they keep pushing each other to grow.")
        ],
        Schedule:
        [
            new("5:00 PM", "Ceremony", "Lakeside Wedding Venue"),
            new("5:30 PM", "Reception", "Lakeside Wedding Venue"),
            new("Evening", "Dinner, Music & Celebration", "Formal reception with room for laughter, stories, and song requests")
        ],
        Faqs:
        [
            new("What should I wear?", "Formal attire is requested."),
            new("Can I bring a guest?", "Guests are by invitation only. Please refer to your invitation for the guests included in your party."),
            new("Is the wedding indoors or outdoors?", "The celebration will include both indoor and outdoor spaces."),
            new("Where should I park?", "Parking is available at the front of the venue inside the gate."),
            new("When should I RSVP?", "Please RSVP by February 28, 2027.")
        ]);
}

public sealed record StoryMoment(string Year, string Title, string Description);

public sealed record ScheduleItem(string Time, string Title, string Location);

public sealed record FaqItem(string Question, string Answer);
