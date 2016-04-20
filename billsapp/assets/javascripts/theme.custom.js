/* Add here all your JS customizations */

/* GitHub Activity Stream */
$(document).ready(function () {
    GitHubActivity.feed({
        username: "levans88",
        //repository: "your-repo", // optional
        selector: "#feed"
        //limit: 20 // optional
    });
});