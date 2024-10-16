using System;
using System.Text;

public class Report
{
    public string Header { get; set; }
    public string Content { get; set; }
    public string Footer { get; set; }

    public override string ToString()
    {
        return $"{Header}\n{Content}\n{Footer}";
    }
}

public interface IReportBuilder
{
    void SetHeader(string header);
    void SetContent(string content);
    void SetFooter(string footer);
    Report GetReport();
}

public class TextReportBuilder : IReportBuilder
{
    private Report report = new Report();

    public void SetHeader(string header) => report.Header = header;
    public void SetContent(string content) => report.Content = content;
    public void SetFooter(string footer) => report.Footer = footer;
    public Report GetReport() => report;
}

public class HtmlReportBuilder : IReportBuilder
{
    private Report report = new Report();

    public void SetHeader(string header) => report.Header = $"<h1>{header}</h1>";
    public void SetContent(string content) => report.Content = $"<p>{content}</p>";
    public void SetFooter(string footer) => report.Footer = $"<footer>{footer}</footer>";
    public Report GetReport() => report;
}

public class ReportDirector
{
    public void ConstructReport(IReportBuilder builder)
    {
        builder.SetHeader("Отчет");
        builder.SetContent("Это содержимое отчета.");
        builder.SetFooter("Конец отчета.");
    }
}

class Program
{
    static void Main()
    {
        var director = new ReportDirector();

        var textBuilder = new TextReportBuilder();
        director.ConstructReport(textBuilder);
        var textReport = textBuilder.GetReport();
        Console.WriteLine("Текстовый отчет:");
        Console.WriteLine(textReport);

        var htmlBuilder = new HtmlReportBuilder();
        director.ConstructReport(htmlBuilder);
        var htmlReport = htmlBuilder.GetReport();
        Console.WriteLine("\nHTML-отчет:");
        Console.WriteLine(htmlReport);
    }
}
