# CS410-Project

## Repository Purpose:

This repository will be used for the CS410 Games Project.



## Trello Link and Card Format:

https://trello.com/b/daubi2YE/cs410-project

Issue Number - Issue Title - Initial of who's working on it

### Examples:
 - 4 - world rotation - RK
 - 1 - first meeting

## Development Workflow
1. Identify the issue you will be working on in the trello board. If one doesn't exist please create one. Feel free to use the template from above.
2. Open a terminal and navigate to this repo.  OR Open this repository in Git Desktop
3. Confirm you are on the main branch.
4. Run the following command: 'git pull'  OR Fetch and Pull in Git Desktop
5. Create a new branch and switch to it using the following command: 'git checkout -b <branch-name>'  OR Click on "Current Branch", click on "New branch", name it, then click "Create branch" 
     -  Example branch-name fb-1, where fb stands for feature branch and 1 represents the issue/card you are working on in the trello board.
6. Now that you're on your feature branch you can start working on your code changes!
7. In order to not have crazy merge conflicts, when working in unity please work in your own build file.
     -  When implemenations pass testing they will be merged into the main build file.
8. Once you have finished with your code changes, build the entire package and make sure existing and new unit tests are passing. This will ensure that we are not introducing any unknown errors.
9. Push your code 'git push'   OR Push on Git Desktop. Since you're on a different branch your terminal will actually prompt you with another command that will look similar to this 'git push --set-upstream <branch_name>' use this.
10. Navigate to the repo on github.
11. You will now be prompted to create a pull-request, do this.
12. For the description of the PR feel free to just link the trello issue/card you were working on and if possible explain how you have verified that your code changes work (i.e. a screenshot, passing unit tests, etc).
13. Once you're ready create the PR and wait for at least 1 person to take a look at it before merging (we don't have to stick to this but it's good practice and it'll help us from making dumb mistakes)

## File Management

### Documentation
Any documentation or class deliverables should be located in the 'Documentation' folder.

### Builds
Any builds should be labeled after the Trello Issue and will be located in the 'Builds' folder.





