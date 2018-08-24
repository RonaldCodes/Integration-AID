namespace UploadLocations.Csv
{
    public class CsvConfiguration
    {
        public CsvConfiguration()
        {
            Delimeter = ";";
            HasHeaders = false;
        }

        public string Delimeter { get; set; }
        public bool HasHeaders { get; set; }
    }
}
