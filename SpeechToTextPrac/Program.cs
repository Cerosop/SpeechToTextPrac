using System;
using System.Threading.Tasks;
using Microsoft.CognitiveServices.Speech;

namespace SpeechToTextPrac
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var config = SpeechConfig.FromSubscription("c25036fff0d240e58d972abd365e6b7c", "eastus");
            
            while (true)
            {
                using (var recognizer = new SpeechRecognizer(config))
                {
                    Console.WriteLine("press enter");
                    string text1 = Console.ReadLine();
                    // Starts recognizing.
                    Console.WriteLine("Say something...");
                    var result = await recognizer.RecognizeOnceAsync().ConfigureAwait(false);

                    // Checks result.
                    if (result.Reason == ResultReason.RecognizedSpeech)
                    {
                        var language = "ja-JP";
                        config.SpeechSynthesisLanguage = language;
                        Console.WriteLine($"RECOGNIZED: Text = {result.Text}");
                        if(result.Text == "Who are you?")
                        {
                            using (var synthesizer = new SpeechSynthesizer(config))
                            {
                                string text = " 私は Kugimiya Rie";

                                using (var results = await synthesizer.SpeakTextAsync(text))
                                {
                                    Console.WriteLine($"Speech synthesized to speaker for text [{text}]");
                                }
                            }
                        }
                        else if (result.Text == "Do something.")
                        {
                            using (var synthesizer = new SpeechSynthesizer(config))
                            {
                                string text = "baka hentai urusai moushiranai!";

                                using (var results = await synthesizer.SpeakTextAsync(text))
                                {
                                    Console.WriteLine($"Speech synthesized to speaker for text [{text}]");
                                }
                            }
                        }
                        else if(result.Text == "You're good." || result.Text == "You are good.")
                        {
                            using (var synthesizer = new SpeechSynthesizer(config))
                            {
                                string text = "べ! betsuni~";

                                using (var results = await synthesizer.SpeakTextAsync(text))
                                {
                                    Console.WriteLine($"Speech synthesized to speaker for text [{text}]");
                                }
                            }
                        }
                        else
                        {
                            using (var synthesizer = new SpeechSynthesizer(config))
                            {
                                string text = "nani itendayo baka!";

                                using (var results = await synthesizer.SpeakTextAsync(text))
                                {
                                    Console.WriteLine($"Speech synthesized to speaker for text [{text}]");
                                }
                            }
                        }
                    }
                    else if (result.Reason == ResultReason.NoMatch)
                    {
                        Console.WriteLine($"NOMATCH: Speech could not be recognized.");
                    }
                    else if (result.Reason == ResultReason.Canceled)
                    {
                        var cancellation = CancellationDetails.FromResult(result);
                        Console.WriteLine($"CANCELED: Reason={cancellation.Reason}");

                        if (cancellation.Reason == CancellationReason.Error)
                        {
                            Console.WriteLine($"CANCELED: ErrorCode={cancellation.ErrorCode}");
                            Console.WriteLine($"CANCELED: ErrorDetails={cancellation.ErrorDetails}");
                            Console.WriteLine($"CANCELED: Did you update the subscription info?");
                        }
                    }
                }
            }
        }
    }
}
//test