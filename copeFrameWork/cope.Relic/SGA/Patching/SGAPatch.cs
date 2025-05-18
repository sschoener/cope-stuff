using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace cope.Relic.SGA.Patching
{
    public class SGAPatch
    {
        public SGAPatch(string name, string sgaName, IEnumerable<SGAFilePatch> patches)
        {
            Name = name;
            SGAFileName = sgaName;
            FilePatches = patches.ToArray();
        }

        /// <summary>
        /// Gets the files patches to apply.
        /// </summary>
        public SGAFilePatch[] FilePatches { get; private set; }

        /// <summary>
        /// Gets the name of this patch.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the name of the archive to patch.
        /// </summary>
        public string SGAFileName { get; private set; }

        /// <summary>
        /// Returns whether or not this patch is applicable.
        /// </summary>
        /// <param name="ep"></param>
        /// <returns></returns>
        public Either<List<string>, bool> IsApplicable(SGAEntryPoint ep)
        {
            var errors = (from patch in FilePatches select patch.IsApplicable(ep) into e where e.IsLeft select e.Left.Value).ToList();
            if (errors.Count > 0)
                return new EitherLeft<List<string>, bool>(errors);
            return new EitherRight<List<string>, bool>(true);
        }

        /// <summary>
        /// Applies this patch to a stream which must represent an archive. That stream must be both read AND write.
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public Either<List<String>, bool> ApplyPatch(Stream stream)
        {
            long basePos = stream.Position;

            var sgaEps = SGAReader.Read(stream);
            var ep = sgaEps[0];

            // can this patch even be applied?
            var applicable = IsApplicable(ep);
            if (applicable.IsLeft)
                return applicable;

            foreach (var patch in FilePatches)
            {
                var offset = ep.GetOffsetInArchive(patch.FileName);
                stream.Position = basePos + offset;
                stream.Write(patch.ReplaceWith, 0, patch.ReplaceWith.Length);
            }
            stream.Flush();
            return new EitherRight<List<String>, bool>(true);
        }
    }
}