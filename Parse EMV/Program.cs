using System;
using System.Collections.Generic;

class TLV
{
    public string Id { get; set; }
    public string Length { get; set; }
    public string Value { get; set; }
    public string Name { get; set; }
    public string TagIdSecondHalf { get; set; }
}

class Program
{
    static void Main()
    {
        string emvData = "9F0607A000000003201057114507788153899851D2411226100006740F5A0845077881538998515F3401019F2701809F26084657B7AAD0991B609F100706010A03A0080082023C009F360200078C159F02069F03069F1A0295055F2A029A039C019F37049F02060000000000009F03060000000000009F1A020414950580000480005F2A0204149A032105249C01019F3704E44839E49F33036040209B02680050105669736120456C656374726F6E2044499F1E083535353335323138";

        List<TLV> emvTags = new List<TLV>();

        emvTags.Add(new TLV { Id = "9F06", Name = "Application Identifier (AID)" });
        emvTags.Add(new TLV { Id = "5F34", Name = "Application Transaction Counter (ATC)" });
        emvTags.Add(new TLV { Id = "9F27", Name = "Cryptogram Information Data" });
        emvTags.Add(new TLV { Id = "9F26", Name = "Application Cryptogram" });
        emvTags.Add(new TLV { Id = "9F10", Name = "Issuer Application Data" });
        emvTags.Add(new TLV { Id = "9F36", Name = "Application Transaction Counter (ATC) (Extended)" });
        emvTags.Add(new TLV { Id = "9F02", Name = "Amount, Authorized (Numeric)" });
        emvTags.Add(new TLV { Id = "9F03", Name = "Amount, Other (Numeric)" });
        emvTags.Add(new TLV { Id = "9F1A", Name = "Terminal Country Code" });
        emvTags.Add(new TLV { Id = "5F2A", Name = "Transaction Currency Code" });
        emvTags.Add(new TLV { Id = "9F37", Name = "Unpredictable Number" });
        emvTags.Add(new TLV { Id = "9F33", Name = "Terminal Capabilities" });
        emvTags.Add(new TLV { Id = "9F1E", Name = "Interface Device (IFD) Serial Number" });
        emvTags.Add(new TLV { Id = "57", Name = "Track Data" });
        emvTags.Add(new TLV { Id = "5A", Name = "Application PAN" });
        emvTags.Add(new TLV { Id = "82", Name = "Application Interchange Profile" });
        emvTags.Add(new TLV { Id = "8C", Name = "Card Risk Management Data" });
        emvTags.Add(new TLV { Id = "95", Name = "Terminal Verification Results (TVR)" });
        emvTags.Add(new TLV { Id = "9A", Name = "Transaction Date" });
        emvTags.Add(new TLV { Id = "9C", Name = "Transaction Type" });
        emvTags.Add(new TLV { Id = "9B", Name = "Transaction Status Information" });
        emvTags.Add(new TLV { Id = "50", Name = "Application Label" });

        (List<TLV> non9F5FTags, List<TLV> _9F5FTags) = ParseEMVData(emvData, emvTags);

        // Print the parsed tags
        Console.WriteLine("SPECS Tags:");
        foreach (var tag in non9F5FTags)
        {
            Console.WriteLine($"Tag ID: {tag.Id}");
            Console.WriteLine($"Tag Name: {tag.Name}");
            Console.WriteLine($"Tag Length: {tag.Length}");
            Console.WriteLine($"Tag Value: {tag.Value}");
        }

        Console.WriteLine("\nEMV Tags:");
        foreach (var tag in _9F5FTags)
        {
            Console.WriteLine($"Tag ID: {tag.Id}");
            Console.WriteLine($"Tag Name: {tag.Name}");
            Console.WriteLine($"Tag Length: {tag.Length}");
            Console.WriteLine($"Tag Value: {tag.Value}");
        }
        Console.ReadKey();
    }

    static (List<TLV>, List<TLV>) ParseEMVData(string emvData, List<TLV> emvTags)
    {
        List<TLV> non9F5FTags = new List<TLV>();
        List<TLV> _9F5FTags = new List<TLV>();
        int index = 0;

        while (index < emvData.Length)
        {
            string tagId = emvData.Substring(index, 2);
            index += 2;

            string tagIdSecondHalf = "";

            if (tagId == "9F" || tagId == "5F")
            {
                tagIdSecondHalf = emvData.Substring(index, 2);
                index += 2;
                tagId = tagId + tagIdSecondHalf;
            }

            string name = "";
            foreach (var tag in emvTags)
            {
                if (tagId == tag.Id)
                {
                    name = tag.Name;
                }
            }

            string tagLengthStr = emvData.Substring(index, 2);
            index += 2;
            string tagLength = tagLengthStr.PadLeft(2, '0'); // Ensure two-character representation

            int tagLengthValue = Convert.ToInt32(tagLength, 16); // Store the actual integer value if needed
            string tagValue = emvData.Substring(index, tagLengthValue * 2);

            index += tagLengthValue * 2;

            TLV emvTag = new TLV
            {
                Id = tagId,
                TagIdSecondHalf = tagIdSecondHalf,
                Length = tagLength,
                Value = tagValue,
                Name = name
            };
            if (tagId.StartsWith("9F") || tagId.StartsWith("5F"))
            {
                _9F5FTags.Add(emvTag);
            }
            else
            {
                non9F5FTags.Add(emvTag);
            }

        }

        return (non9F5FTags, _9F5FTags);
    }
}
