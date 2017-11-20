using System;
using System.IO;
using Newtonsoft.Json;

namespace InfiniteGravity.Configuration {
    public class GameContext {
        public readonly GameConfiguration configuration;

        public GameContext(GameConfiguration configuration) {
            this.configuration = configuration;
        }
    }

    public class GameConfiguration {
        private string _boundFilename = null;

        public GraphicsConfiguration graphics { get; } = new GraphicsConfiguration
            { };

        public class GraphicsConfiguration {
            public bool fullscreen = false;
            public ScaleMode scaleMode = ScaleMode.PixelPerfect;

            public enum ScaleMode {
                PixelPerfect,
                Stretch
            };
        }

        public void bind(string filename) {
            _boundFilename = filename;
        }

        public void save() {
            if (_boundFilename == null)
                throw new InvalidOperationException(
                    "Configuration not bound to file. Use bind(filename) to bind to a file.");
            // write out to binded file
            File.WriteAllText(_boundFilename, JsonConvert.SerializeObject(this, Formatting.Indented));
        }
    }
}