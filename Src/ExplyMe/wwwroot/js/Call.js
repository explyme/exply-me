window.StartCall = function (token, roomName) {
    Twilio.Video.createLocalTracks({
        audio: true,
        video: { width: 640 }
    }).then(localTracks => {
        const localMediaContainer = document.getElementById('remote-video');
        localMediaContainer.appendChild(localTracks[1].attach()); // Video track

        return Twilio.Video.connect(token, {
            name: roomName,
            tracks: localTracks
        });
    }).then(room => {
        console.log(`Connected to Room: ${room.name}`);
    });
};