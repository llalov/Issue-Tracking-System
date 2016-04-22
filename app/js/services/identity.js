angular.module('issueTrackingSystem.users.identity',[])
    .factory('identity', [function(){
        var accessToken = "2c2ZPJFZaPV7XNlXnutGRUPCvg_j9PTwY8Uj5UDxE9N2h3EmbTnSH1frbgEseBENwWMvMbAJBSHDAhXe29DteJfcRfjiprNPnN5Vb3YMDmmH0P1SdoN_7PUAf2VhKDeh5bZs0ONleO0G0efPLx0jw9TU6FLK2sNCzyg5Ct2pMsIzQXrlj4rQx_dAKyiPcqc0i-Zge71onXZIdPoKVUeY2HkCR-7CVPtDA3mFfd8Ck_GhH1wVVOnmu3vH8auOCvX6DnhuYA4xoTddCdmfwYe8HV1K0Wf2vGLJVNh2RgCJpSg14PpuMOQhxynl39OCmIqxCVbhC7RwiXjNgMqRIYWyJEmc74Xy_PCy_IuQ9CWmyxZsp1PWrAfxt4UTsgAMeSN033aDThL9NcrJHFc42qmPEaXr-gVLjY6VPU7wlN7cm8n3233iQR659qDmHfJR251MM6pMSg5v4uxiWhTip8yP7gQs0MZd1Qjh-WG3kJU-hKhLm0IXBAtMKYw5u6E6E-DP";
        var username = "aas@abv.bg";

        return {
            getCurrentUser: function () {
                return {
                    username: username
                }
            },
            isAuthenticated: function () {
                return true;
            }
        }
    }]);
