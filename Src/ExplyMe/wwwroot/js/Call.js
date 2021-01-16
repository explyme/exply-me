window.StartCall = function () {
    Twilio.Video.createLocalTracks({
        audio: true,
        video: { width: 640 }
    }).then(localTracks => {
        const localMediaContainer = document.getElementById('remote-video');
        localMediaContainer.appendChild(localTracks[1].attach()); // Video track

        return connect('$TOKEN', {
            name: 'my-room-name',
            tracks: localTracks
        });
    }).then(room => {
        console.log(`Connected to Room: ${room.name}`);
    });
};