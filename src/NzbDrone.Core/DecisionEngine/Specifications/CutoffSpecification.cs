using System;
using System.Linq;
using NLog;
using NzbDrone.Core.IndexerSearch.Definitions;
using NzbDrone.Core.Parser.Model;
using NzbDrone.Core.MediaFiles;

namespace NzbDrone.Core.DecisionEngine.Specifications
{
    public class CutoffSpecification : IDecisionEngineSpecification
    {
        private readonly UpgradableSpecification _upgradableSpecification;
        private readonly IMediaFileService _mediaFileService;
        private readonly Logger _logger;

        public CutoffSpecification(UpgradableSpecification upgradableSpecification, Logger logger, IMediaFileService mediaFileService)
        {
            _upgradableSpecification = upgradableSpecification;
            _logger = logger;
            _mediaFileService = mediaFileService;
        }

        public SpecificationPriority Priority => SpecificationPriority.Default;
        public RejectionType Type => RejectionType.Permanent;

        public virtual Decision IsSatisfiedBy(RemoteAlbum subject, SearchCriteriaBase searchCriteria)
        {

            foreach (var album in subject.Albums)
            {
                var trackFiles = _mediaFileService.GetFilesByAlbum(album.ArtistId, album.Id);

                if (trackFiles.Any())
                {
                    var lowestQuality = trackFiles.Select(c => c.Quality).OrderBy(c => c.Quality.Id).First();

                    _logger.Debug("Comparing file quality and language with report. Existing file is {0}", lowestQuality.Quality);

                    if (!_upgradableSpecification.CutoffNotMet(subject.Artist.Profile,
                                                               subject.Artist.LanguageProfile,
                                                               lowestQuality,
                                                               trackFiles[0].Language,
                                                               subject.ParsedAlbumInfo.Quality))
                    {
                        _logger.Debug("Cutoff already met, rejecting.");
                        return Decision.Reject("Existing file meets cutoff: {0}", subject.Artist.Profile.Value.Cutoff);
                    }

                }
            }

            return Decision.Accept();
        }
    }
}
