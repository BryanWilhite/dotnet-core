/// <reference path="./node_modules/@types/node/index.d.ts" />
module.exports = function(callback, name) {
    var result = `Hello from Node, ${name}`;
    callback( /* error */ null, result);
}
