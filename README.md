# YoutubeSongsDownloader

## Create the list of the songs
1. Sign in shazam.com with fb account
1. Check in "G:\My Drive\OneS\Skoda USB Audio\Syncs" what was the last checked song of the Shazam's list
    - Open checked list (usually under name "songs urls.txt") of the LAST TIME you have downloaded the songs
    - Scroll backward (down) in the list of all the songs in https://www.shazam.com/gb/myshazam and find the order number of the latest checked song
    - Uncomment the first row in the script below and put the number from the previous point into the variable number_of_latest_downloaded_and_shazamed_song
3. Run the script:
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

## Download the songs in the created lists
1. Run the tool YoutubeSongsDownloader for each text file that contains a list with Youtube links.
1. In Desktop's folder Downloaded could be found the downloaded songs in subfolders corresponding to the name of txt list file.
1. Use this online tool to cut mp3s - https://mp3cut.net/

## Dowload using web apps
When there are any songs that aren't successfully downloaded observe the log.txt file and download them separetely:
1. Filter songs from the log by executing the command 
    ```console
    findstr "Error with:" [path_to_log.txt_file] > [output_file]
    ```
1. Use some web app to download songs (preferable at least 320 kbps quality)
    - https://yt2mp3.info/
    - https://320ytmp3.com/en18/
    - https://ytmp3.cc/en40/
