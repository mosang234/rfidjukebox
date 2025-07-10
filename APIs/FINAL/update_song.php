<?php 

include_once('connects.php');
$songnumber = $_GET['songnumber'];


$result = mysqli_query($con,"UPDATE song SET songnumber = '$songnumber';");
echo "New Song Played!";

mysqli_close($con);
?>