using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ML;
using Microsoft.ML.Data;
using Sprint4.Models; // Ajuste conforme sua estrutura de projeto

namespace Sprint4.Services
{
    // Classe que representa a entrada de texto para análise de sentimento
    public class SentimentInput
    {
        public string Text { get; set; }
    }

    // Classe que representa o resultado da análise de sentimento
    public class SentimentPrediction
    {
        [ColumnName("PredictedLabel")]
        public bool IsPositive { get; set; }

        [ColumnName("Probability")]
        public float Probability { get; set; }
    }

    public class SentimentService
    {
        private readonly MLContext _mlContext;
        private readonly ITransformer _model;

        public SentimentService(string modelPath)
        {
            _mlContext = new MLContext();

            // Carregar o modelo previamente treinado
            _model = _mlContext.Model.Load(modelPath, out var _);
        }

        public async Task<SentimentPrediction> AnalyzeSentimentAsync(string text)
        {
            var input = new SentimentInput { Text = text };

            // Fazer a previsão
            var predictionEngine = _mlContext.Model.CreatePredictionEngine<SentimentInput, SentimentPrediction>(_model);
            var prediction = predictionEngine.Predict(input);

            return prediction;
        }
    }
}

