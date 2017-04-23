/// <binding AfterBuild='build-dev' ProjectOpened='build-dev, materialize-amd' />
/*
This file in the main entry point for defining Gulp tasks and using Gulp plugins.
Click here to learn more. http://go.microsoft.com/fwlink/?LinkId=518007
*/

var gulp = require('gulp');
var shell = require('gulp-shell');
var download = require("gulp-download");

gulp.task('_get-rbuild',
    function () {
        return download(
            'https://raw.githubusercontent.com/aurelia-ui-toolkits/aurelia-materialize-bridge/master/build/tools/rbuild.js')
            .pipe(gulp.dest('./'));
    });
gulp.task('_get-materialize-css',
    function () {
        return download(
            'https://raw.githubusercontent.com/aurelia-ui-toolkits/aurelia-materialize-bridge/master/build/tools/materialize-css.js')
            .pipe(gulp.dest('./'));
    });
gulp.task('_materialize-amd', shell.task(['node_modules\\.bin\\r.js.cmd -o rbuild.js']));

gulp.task('build-dev', shell.task(['au build --env dev']));
gulp.task('build-prod', shell.task(['au build --env prod']));
gulp.task('build-stage', shell.task(['au build --env stage']));
gulp.task('watch', shell.task(['au run --watch']));
gulp.task('test', shell.task(['au test']));
gulp.task('prepare-project', gulp.series('_get-rbuild', '_get-materialize-css', '_materialize-amd'));


console.log('--- NOTICE ---'
    + '\n'
    + 'If these tasks fail due to an error "Error: spawn au build --env dev ENOENT", go to "Tools > Options > Projects and Solutions > External Web Tools" '
    + 'and move your $(PATH) entry above the $(VSINSTALLDIR) entries.'
    + '\n\n'
    + 'Visual Studio comes bundled with an old version of node which does not support the spawning of the aurelia build task.'
    + '\n\n'
    + 'See: https://blogs.msdn.microsoft.com/webdev/2015/03/19/customize-external-web-tools-in-visual-studio-2015/ for more details.'
    + '\n'
    + '--------------');