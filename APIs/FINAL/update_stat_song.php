<?php 

include_once('connects.php');
$status_song = $_GET['status_song'];


$result = mysqli_query($con,"UPDATE button SET status_song = '$status_song';");
echo "Song status changed!";

?>