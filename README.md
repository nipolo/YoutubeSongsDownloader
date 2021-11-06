# YoutubeSongsDownloader

1. Sign in shazam.com with fb account
1. Scroll to load all shazam results up to the latest that is on the flash drive and remember the number of the song.
1. Run the script:
    ```js
    //var number_of_latest_downloaded_and_shazamed_song = 714;
    var allSongsNameAndArtistSeparated = Array.from(
      document.getElementsByClassName("ellip")
    )
      .slice(1, 2 * number_of_latest_downloaded_and_shazamed_song)
      .map((a) => a.innerText);
    var allSongsNameAndArtist = [];
    
    for (
      var songIndex = 0;
      songIndex < allSongsNameAndArtistSeparated.length - 1;
      songIndex += 2
    ) {
      var link = "https://www.youtube.com/results?search_query=";
      var fullSongName =
        allSongsNameAndArtistSeparated[songIndex] +
        " - " +
        allSongsNameAndArtistSeparated[songIndex + 1];
      allSongsNameAndArtist.push(
        link +
          fullSongName
            .split(" ")
            .reduce(
              (accumulator, currentValue) => (accumulator += currentValue + "+"),
              ""
            )
            .slice(0, -1)
      );
    }
    console.log(allSongsNameAndArtist.reduce(
      (accumulator, currentValue) => (accumulator += currentValue + "\n"),
      ""
    ));
    ```
1. Copy and paste the list of "[Song name] - [artist name]" in text file
1. Iterate through this list and find song in youtube and make new list of desired songs and copy and paste the link in text file named songs.txt (for each genre could have different text file of youtube links of the songs).
1. Run the tool YoutubeSongsDownloader for each text file that contains a list with Youtube links.
1. In Desktop's folder Downloaded could be found the downloaded songs in subfolders corresponding to the name of txt list file.
1. Use this online tool to cut mp3s - https://mp3cut.net/
