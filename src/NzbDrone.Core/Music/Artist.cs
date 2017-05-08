﻿using Marr.Data;
using NzbDrone.Common.Extensions;
using NzbDrone.Core.Datastore;
using NzbDrone.Core.Profiles;
using NzbDrone.Core.Tv;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NzbDrone.Core.Music
{
    public class Artist : ModelBase
    {
        public Artist()
        {
            Images = new List<MediaCover.MediaCover>();
            Genres = new List<string>();
            //Members = new List<Person>(); // Artist Band Member? (NOTE: This should be per album)
            Albums = new List<Album>();
            Tags = new HashSet<int>();

        }

        public string SpotifyId { get; set; }
        public string ArtistName { get; set; }
        public string ArtistSlug { get; set; }
        public string CleanTitle { get; set; }
        public string Overview { get; set; }
        public bool Monitored { get; set; }
        public bool AlbumFolder { get; set; }
        public bool ArtistFolder { get; set; }
        public DateTime? LastInfoSync { get; set; }
        public DateTime? LastDiskSync { get; set; }
        public int Status { get; set; } // TODO: Figure out what this is, do we need it? 
        public string Path { get; set; }
        public List<MediaCover.MediaCover> Images { get; set; }
        public List<string> Genres { get; set; }
        public int QualityProfileId { get; set; }
        public string RootFolderPath { get; set; }
        public DateTime Added { get; set; }
        public LazyLoaded<Profile> Profile { get; set; }
        public int ProfileId { get; set; }
        public List<Album> Albums { get; set; }
        public HashSet<int> Tags { get; set; }
        public AddSeriesOptions AddOptions { get; set; }

        public override string ToString()
        {
            return string.Format("[{0}][{1}]", SpotifyId, ArtistName.NullSafe());
        }

        public void ApplyChanges(Artist otherArtist)
        {

            SpotifyId = otherArtist.SpotifyId;
            ArtistName = otherArtist.ArtistName;
            ArtistSlug = otherArtist.ArtistSlug;
            CleanTitle = otherArtist.CleanTitle;
            Monitored = otherArtist.Monitored;
            AlbumFolder = otherArtist.AlbumFolder;
            LastInfoSync = otherArtist.LastInfoSync;
            Images = otherArtist.Images;
            Path = otherArtist.Path;
            Genres = otherArtist.Genres;
            RootFolderPath = otherArtist.RootFolderPath;
            Added = otherArtist.Added;
            Profile = otherArtist.Profile;
            ProfileId = otherArtist.ProfileId;
            Albums = otherArtist.Albums;
            Tags = otherArtist.Tags;
            ArtistFolder = otherArtist.ArtistFolder;
            AddOptions = otherArtist.AddOptions;

            Albums = otherArtist.Albums;
            Path = otherArtist.Path;
            ProfileId = otherArtist.ProfileId;
            AlbumFolder = otherArtist.AlbumFolder;
            Monitored = otherArtist.Monitored;
            RootFolderPath = otherArtist.RootFolderPath;
            Tags = otherArtist.Tags;
            AddOptions = otherArtist.AddOptions;

        }
    }
}
