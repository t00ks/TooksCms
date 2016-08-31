/// <binding AfterBuild='less' ProjectOpened='watch' />

module.exports = function (grunt) {
    grunt.initConfig({
        durandal: {
            dist: {
                src: [
                    'app/**/*.*',
                    'scripts/lib/durandal/**/*.*'
                ],
                options: {
                    uglify2: {
                        compress: {
                            global_defs: {
                                DEBUG: false
                            }
                        }
                    }

                }

            }
        },
        less: {
            site: {
                options: {
                    compress: false,
                    yuicompress: false,
                    optimization: 2,
                    sourceMap: true,
                    sourceMapFilename: "content/tk/site.css.map",
                    relativeUrls: true
                    //sourceMapRootpath: "content/px/"
                },
                files: {
                    "content/tk/site.css": "content/tk/site.less"
                }
            },
            bootstrap: {
                options: {
                    compress: true,
                    yuicompress: true,
                    optimization: 2,
                    relativeUrls: true
                },
                files: {
                    "content/bootstrap/bootstrap.css": "content/bootstrap/bootstrap.less",
                }
            },
            toastr: {
                options: {
                    compress: true,
                    yuicompress: true,
                    optimization: 2,
                    sourceMap: true,
                    sourceMapFilename: "content/toastr.css.map",
                    sourceMapBasepath: "content"
                },
                files: {
                    "content/toastr.css": "content/toastr.less",
                }
            }
        },
        watch: {
            files: "content/tk/**/*.less",
            tasks: ["less"],
            options: {
                nospawn: true
            }
        },
    });

    //grunt.loadTasks('tasks');

    grunt.loadNpmTasks('grunt-contrib-less');
    grunt.loadNpmTasks('grunt-contrib-watch');

    grunt.registerTask('default', ['less', 'durandal']);
};