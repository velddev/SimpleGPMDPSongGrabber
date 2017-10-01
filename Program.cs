using System;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace PlayMusicTracker
{
	class Program
	{
		static void Main(string[] args)
		{
			FileSystemWatcher fsWatch = 
				new FileSystemWatcher(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Google Play Music Desktop Player\json_store", "*.json");

			fsWatch.Changed += (sender, newfile) =>
			{
				try
				{
					StreamReader fs = new StreamReader(newfile.FullPath);
					string output = fs.ReadToEnd();
					fs.Close();

					PlaybackApi d = JsonConvert.DeserializeObject<PlaybackApi>(output);
					Console.Title = d.song.artist + " - " + d.song.title;
				}
				catch
				{

				}
			};

			while(true)
			{
				fsWatch.WaitForChanged(WatcherChangeTypes.Changed);
			}
		}
	}

	class PlaybackApi
	{
		public bool playing = false;
		public PlaybackApiSong song;
		public PlaybackApiRating rating;
		public PlaybackApiTime time;
		public string songLyrics;
		public string shuffle;
		public string repeat;
		public int volume;
	}

	class PlaybackApiSong
	{
		public string title;
		public string artist;
		public string album;
		public string albumImageUrl;
	}

	class PlaybackApiRating
	{
		public bool liked;
		public bool disliked;
	}

	class PlaybackApiTime
	{
		public long current;
		public long total;
	}
}
