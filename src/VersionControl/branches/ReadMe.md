# git Branches

- [ ] About git branches
- [x] Tutorial

## Tutorial

A good way to learn about branching and using the **git flow** technique is to run through the visual tool at [git-school.github.io/visualizing-git](http://git-school.github.io/visualizing-git/#free). In this walkthrough, we're going to use the following git commands to see what is happening with the git repository.

- **`git commit -m 'commit message'`** - Save your changes to your git history.
- **`git branch -a`** - List **a**ll the branches in your repository.
- **`git branch -d new-branch`** - Delete the branch called "new-branch".
- **`git branch new-branch`** - Create a new branch from the current *`head`* called "new-branch".
- **`git checkout -b new-branch`** - Create a new branch from the current *`head`* called "new-branch" and switch to that branch.
- **`git checkout new-branch`** - Switch from your current branch to the branch named "new-branch".
- **`git merge new-branch`** - Merge the contents of "new-branch" to the branch you are currently on.

| Command | Result |
|:--------|:-------|
| List the current branches<br/>`git branch -a` | ![Step ](./branch-1-list.png) |
| Update the README file<br/>`git commit -m "Update README"` | ![Step ](./branch-2-commit.png) |
| Create a branch called **v1-0**<br/>`git branch v1-0` | ![Step ](./branch-3-create-branch.png) |
| Switch to branch **v1-0**<br/>`git checkout v1-0` | ![Step ](./branch-4-checkout.png) |
| Commit on the **v1-0** branch<br/>`git commit -m "Plan feature list"` | ![Step ](./branch-5-commit.png) |
| Switch back to the master branch<br/>`git checkout master` | ![Step ](./branch-6-checkout-master.png) |
| Make a commit on the master branch<br/>`git commit -m "Create roadmap for versions 1 and 2"` | ![Step ](./branch-7-commit.png) |
| Switch back to the **v1-0** branch<br/>`git checkout v1-0` | ![Step ](./branch-8-checkout-v1-0.png) |
| Create a feature branch called **feature-A** based on the current branch and switch to this new branch<br/>`git checkout -b feature-A` | ![Step ](./branch-9-checkout-new-branch.png) |
| Make a commit<br/>`git commit -m "plan feature"` | ![Step ](./branch-a-commit.png) |
| Make another commit<br/>`git commit -m "start feature"` | ![Step ](./branch-b-commit.png) |
| Switch to the **v1-0** branch<br/>`git checkout v1-0` | ![Step ](./branch-c-checkout.png) |
| Make a commit<br/>`git commit -m "update feature list"` | ![Step ](./branch-d-commit.png) |
| Switch to the **feature-A** branch<br/>`git checkout feature-A` | ![Step ](./branch-e-checkout.png) |
| Make a final commit on this branch<br/>`git commit -m "finish feature"` | ![Step ](./branch-f-commit.png) |
| Switch to the **v1-0** branch<br/>`git checkout v1-0` | ![Step ](./branch-g-checkout.png) |
| Merge in the changes from the **feature-A** branch into the **v1-0** branch<br/>`git merge feature-A` | ![Step ](./branch-h-merge.png) |
| Make a commit<br/>`git commit -m "update roadmap"` | ![Step ](./branch-i-commit.png) |
| Swith to the **master** branch<br/>`git checkout master` | ![Step ](./branch-j-checkout.png) |
| Merge the **v1-0** changes into the **master** branch<br/>`git merge v1-0` | ![Step ](./branch-k-merge.png) |
| Add a tag to the current commit<br/>`git tag v1.0` | ![Step ](./branch-l-tag.png) |
| Delete the feature branch<br/>`git branch -d feature-A` | ![Step ](./branch-m-delete-branch.png) |
| Switch to the **v1-0** branch<br/>`git checkout v1-0` | ![Step ](./branch-n-checkout.png) |
| Add a commit about a bug report<br/>`git commit -m "bug report"` | ![Step ](./branch-o-commit.png) |
| List the branches<br/>`git branch -a` | ![Step ](./branch-p-list.png) |
