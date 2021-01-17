window.StartCall = function (token, roomName) {
    Twilio.Video.createLocalTracks({
        audio: true,
        video: { width: 640 }
    }).then(localTracks => {
        const localMediaContainer = document.getElementById('local-video');
        localMediaContainer.appendChild(localTracks[1].attach()); // Video track

        return Twilio.Video.connect(token, {
            name: roomName,
            tracks: localTracks
        });
    }).then(room => {
        $("#mute-audio-button").click(function () {
            var isAudioMuted = $("#mute-audio-button").hasClass("muted");

            if (isAudioMuted) {
                room.localParticipant.audioTracks.forEach(publication => {
                    publication.track.enable();
                });

                $("#mute-audio-button").removeClass("muted");
                $("#mute-audio-button i").addClass("bi-mic-fill").removeClass("bi-mic-mute-fill");
            } else {
                room.localParticipant.audioTracks.forEach(publication => {
                    publication.track.disable();
                });

                $("#mute-audio-button").addClass("muted");
                $("#mute-audio-button i").addClass("bi-mic-mute-fill").removeClass("bi-mic-fill");
            }
        });

        $("#mute-video-button").click(function () {
            var isVideoMuted = $("#mute-video-button").hasClass("muted");

            if (isVideoMuted) {
                room.localParticipant.videoTracks.forEach(publication => {
                    console.log("vídeo habilitado");
                    publication.track.enable();
                });

                $("#mute-video-button").removeClass("muted");
                $("#mute-video-button i").addClass("bi-camera-video-fill").removeClass("bi-camera-video-off-fill");
            } else {
                room.localParticipant.videoTracks.forEach(publication => {
                    publication.track.disable();
                });

                $("#mute-video-button").addClass("muted");
                $("#mute-video-button i").addClass("bi-camera-video-off-fill").removeClass("bi-camera-video-fill");
            }
        });

        room.participants.forEach(participant => {
            participant.tracks.forEach(publication => {
                if (publication.track) {
                    handleTrackDisabled(track);
                    document.getElementById('remote-video').appendChild(publication.track.attach());
                }
            });

            participant.on('trackSubscribed', track => {
                handleTrackDisabled(track);
                document.getElementById('remote-video').appendChild(track.attach());
            });
        });

        room.on('participantConnected', participant => {
            participant.tracks.forEach(publication => {
                if (publication.isSubscribed) {
                    const track = publication.track;
                    handleTrackDisabled(track);
                    document.getElementById('remote-video').appendChild(track.attach());
                }
            });

            participant.on('trackSubscribed', track => {
                handleTrackDisabled(track);
                document.getElementById('remote-video').appendChild(track.attach());
            });
        });

        room.once('participantDisconnected', participant => {
            console.log(`Participant "${participant.identity}" has disconnected from the Room`);
        });

        console.log(`Connected to Room: ${room.name}`);
    });
};

function handleTrackDisabled(track) {
    $("#waiting-participant").hide();

    track.on('enabled', (eventTrack) => {
        console.log(eventTrack.kind);
        if (eventTrack.kind == "video") {
            $("#remote-video video").show();
            $("#no-cam").hide();
        }

        if (eventTrack.kind == "audio")
            $("#user-info i").hide();
    });

    track.on('disabled', (eventTrack) => {
        if (eventTrack.kind == "video") {
            $("#remote-video video").hide();
            $("#no-cam").show();
        }

        if (eventTrack.kind == "audio")
            $("#user-info i").show();
    });
}