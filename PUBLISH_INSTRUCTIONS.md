# Publishing to GitHub Instructions

## Step 1: Create a GitHub Repository

1. Go to [GitHub.com](https://github.com) and sign in to your account
2. Click the "+" icon in the top right corner and select "New repository"
3. Fill in the repository details:
   - **Repository name**: `jellyfin-plugin-kinopoisk`
   - **Description**: `Jellyfin metadata provider plugin for Kinopoisk API - Russian movie and TV show metadata`
   - **Visibility**: Public (recommended for open source)
   - **Initialize repository**: Leave all checkboxes UNCHECKED (don't add README, .gitignore, or license)

4. Click "Create repository"

## Step 2: Connect and Push Your Local Repository

After creating the repository, GitHub will show you instructions. Use these commands in PowerShell:

```powershell
# Add the remote repository (replace YOUR_USERNAME with your GitHub username)
git remote add origin https://github.com/YOUR_USERNAME/jellyfin-plugin-kinopoisk.git

# Push your code to GitHub
git push -u origin main
```

## Step 3: Create Your First Release

1. Go to your repository on GitHub
2. Click on "Releases" (on the right side of the repository page)
3. Click "Create a new release"
4. Fill in the release details:
   - **Tag version**: `v1.0.0`
   - **Release title**: `v1.0.0 - Initial Release`
   - **Description**: Use the description from the README
5. Click "Publish release"

## Step 4: Verify GitHub Actions

After pushing, GitHub Actions should automatically:
- Build your project on every push
- Create releases when you push tags
- Generate downloadable plugin packages

## Repository Features Included

✅ **Complete Plugin**: Ready-to-use Jellyfin plugin
✅ **Documentation**: Comprehensive README with installation instructions  
✅ **License**: MIT License for open source distribution
✅ **CI/CD**: GitHub Actions for automated building and releases
✅ **Git Ignore**: Proper .gitignore for .NET projects
✅ **API Integration**: Full Kinopoisk API integration with default key

## Quick Commands

```bash
# To create a new release later:
git tag v1.1.0
git push origin v1.1.0

# To update your repository:
git add .
git commit -m "Your commit message"
git push origin main
```

Your plugin is now ready for the GitHub community! 🎉
