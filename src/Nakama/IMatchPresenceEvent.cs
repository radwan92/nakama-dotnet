/**
 * Copyright 2018 The Nakama Authors
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nakama
{
    /// <summary>
    /// A batch of join and leave presences for a match.
    /// </summary>
    public interface IMatchPresenceEvent
    {
        /// <summary>
        /// Presences of users who joined the match.
        /// </summary>
        IEnumerable<IUserPresence> Joins { get; }

        /// <summary>
        /// Presences of users who left the match.
        /// </summary>
        IEnumerable<IUserPresence> Leaves { get; }

        /// <summary>
        /// The unique match identifier.
        /// </summary>
        string MatchId { get; }
    }

    /// <inheritdoc cref="IMatchPresenceEvent"/>
    internal class MatchPresenceEvent : IMatchPresenceEvent
    {
        public IEnumerable<IUserPresence> Joins => _joins ?? UserPresence.NoPresences;
        [DataMember(Name = "joins")] public List<UserPresence> _joins { get; set; }

        public IEnumerable<IUserPresence> Leaves => _leaves ?? UserPresence.NoPresences;
        [DataMember(Name = "leaves")] public List<UserPresence> _leaves { get; set; }

        [DataMember(Name = "match_id")] public string MatchId { get; set; }

        public override string ToString()
        {
            var joins = string.Join(", ", Joins);
            var leaves = string.Join(", ", Leaves);
            return $"MatchPresenceEvent(Joins=[{joins}], Leaves=[{leaves}], MatchId='{MatchId}')";
        }
    }
}