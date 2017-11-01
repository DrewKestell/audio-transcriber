using System;
using Google.Cloud.Speech.V1;

namespace AudioTranscriber
{
    class Program
    {
        static void Main(string[] args)
        {
            var uri = "gs://<bucket-name>/<file-name>";

            var speech = SpeechClient.Create();
            var longOperation = speech.LongRunningRecognize(new RecognitionConfig()
            {
                Encoding = RecognitionConfig.Types.AudioEncoding.Flac,
                SampleRateHertz = 16000,
                LanguageCode = "en",
            }, RecognitionAudio.FromStorageUri(uri));

            longOperation = longOperation.PollUntilCompleted();

            var response = longOperation.Result;

            foreach (var result in response.Results)
            {
                foreach (var alternative in result.Alternatives)
                {
                    Console.WriteLine(alternative.Transcript);
                }
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}